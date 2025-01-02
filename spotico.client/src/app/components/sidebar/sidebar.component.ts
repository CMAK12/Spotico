import { ChangeDetectionStrategy, ChangeDetectorRef, Component, inject, OnInit } from '@angular/core';
import { RouterLink } from '@angular/router';
import { PlaylistService } from '../../services/playlist.service';
import { AuthService } from '../../services/auth.service';
import { PlaylistDTO } from '../../DTOs/playlist.dto';
import { CommonModule } from '@angular/common';
import { Playlist } from '../../models/playlist.model';
import { CustomerService } from '../../services/customer.service';

@Component({
  selector: 'app-sidebar',
  standalone: true,
  imports: [
    RouterLink,
    CommonModule
  ],
  templateUrl: './sidebar.component.html',
  styleUrl: './sidebar.component.scss',
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class SidebarComponent implements OnInit {
  private playlistService = inject(PlaylistService);
  private customerService = inject(CustomerService);
  private authService = inject(AuthService);
  private cdr = inject(ChangeDetectorRef);

  playlists: Playlist[] = [];
  usernames: { [key: string]: string } = {};
  authStatus$ = this.authService.authStatus$;

  ngOnInit(): void {
    this.loadPlaylists();
  }

  createPlaylist(): void {
    // Create a new empty playlist for the user editing
    const newEmptyPlaylist: PlaylistDTO = {
      title: 'New Playlist',
      description: '',
      trackIds: [],
      createdById: this.authService.extractUserId(),
      isPublic: false
    };

    this.playlistService.createPlaylist(newEmptyPlaylist).subscribe(() => {
      // After creating the playlist, reload the playlists list
      this.loadPlaylists();
    });
  }

  private loadPlaylists(): void {
    this.playlistService.getPlaylists().subscribe((playlists1: Playlist[]) => {
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

    this.customerService.getCustomer(userId).subscribe((user) => {
      this.usernames[userId] = user.username;
      this.cdr.markForCheck(); // Trigger change detection
    });
  }
}
