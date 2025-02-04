import {
  ChangeDetectionStrategy,
  ChangeDetectorRef,
  Component,
  DestroyRef,
  inject,
  OnDestroy,
  OnInit,
} from '@angular/core';
import { RouterLink } from '@angular/router';
import { PlaylistService } from '../../services/playlist.service';
import { AuthService } from '../../services/auth.service';
import { PlaylistDTO } from '../../DTOs/playlist.dto';
import { CommonModule } from '@angular/common';
import { Playlist } from '../../models/playlist.model';
import { CustomerService } from '../../services/customer.service';
import { take } from 'rxjs';

@Component({
  selector: 'app-sidebar',
  standalone: true,
  imports: [RouterLink, CommonModule],
  templateUrl: './sidebar.component.html',
  styleUrl: './sidebar.component.scss',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class SidebarComponent implements OnInit, OnDestroy {
  private readonly playlistService = inject(PlaylistService);
  private readonly customerService = inject(CustomerService);
  private readonly authService = inject(AuthService);
  private readonly cdr = inject(ChangeDetectorRef);

  playlists: Playlist[] = [];
  usernames: { [key: string]: string } = {};
  authStatus$ = this.authService.authStatus$;

  private interval: any;

  ngOnInit(): void {
    this.loadPlaylists(); // Initial load
    this.interval = setInterval(() => {
      this.loadPlaylists();
    }, 6000); // Update every 60 seconds
  }

  createPlaylist(): void {
    // Create a new empty playlist for the user editing
    const newEmptyPlaylist: PlaylistDTO = {
      title: 'New Playlist №' + (this.playlists.length + 1),
      description: '',
      trackIds: [],
      createdById: this.authService.extractUserId(),
      isPublic: false,
    };

    this.playlistService.createPlaylist(newEmptyPlaylist).subscribe(() => {
      // After creating the playlist, reload the playlists list
      this.loadPlaylists();
    });
  }

  private loadPlaylists(): void {
    this.playlistService
      .getPlaylists()
      // .pipe(takeUntilDestroyed(this.destroyRef$))
      .subscribe((playlists1: Playlist[]) => {
        this.playlists = playlists1;
        playlists1.forEach((playlist) => {
          this.loadUsername(playlist.createdById);
        });
        this.cdr.markForCheck();
      });
  }

  private loadUsername(userId: string): void {
    if (this.usernames[userId]) {
      // Username is already cached
      return;
    }

    this.customerService
      .getCustomer(userId)
      .pipe(take(1))
      .subscribe((user) => {
        this.usernames[userId] = user.username;
        this.cdr.markForCheck(); // Trigger change detection
      });
  }

  ngOnDestroy(): void {
    clearInterval(this.interval);
  }
}
