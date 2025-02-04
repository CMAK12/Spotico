import { CommonModule } from '@angular/common';
import {
  ChangeDetectionStrategy,
  ChangeDetectorRef,
  Component,
  inject,
  OnInit,
} from '@angular/core';
import { PlaylistService } from '../../services/playlist.service';
import { ActivatedRoute, RouterLink } from '@angular/router';
import { Playlist } from '../../models/playlist.model';
import { take } from 'rxjs';
import { AudioService } from '../../services/audio.service';
import { TimeFormatPipe } from '../../pipes/time-format.pipe';
import { TrackService } from '../../services/track.service';
import { Track } from '../../models/track.model';
import { AlbumService } from '../../services/album.service';
import { Album } from '../../models/album.model';
import { User } from '../../models/user.model';
import { CustomerService } from '../../services/customer.service';
import { PlayerTrack } from '../../DTOs/player-track.dto';

@Component({
  selector: 'app-playlist',
  standalone: true,
  imports: [CommonModule, RouterLink, TimeFormatPipe],
  templateUrl: './playlist.component.html',
  styleUrl: './playlist.component.scss',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class PlaylistComponent implements OnInit {
  private readonly playlistService = inject(PlaylistService);
  private readonly userService = inject(CustomerService);
  private readonly albumService = inject(AlbumService);
  private readonly trackService = inject(TrackService);
  private readonly audioService = inject(AudioService);
  private readonly cdr = inject(ChangeDetectorRef);
  private readonly route = inject(ActivatedRoute);

  // Get the id from the URL
  private readonly currentPlaylistId = this.route.snapshot.paramMap.get('id');
  albumMap: { [key: string]: Album } = {};
  userMap: { [key: string]: User } = {};
  tracks: Track[] = [];
  playlist: Playlist;

  ngOnInit() {
    // Get the playlist
    this.playlistService
      .getPlaylist(this.currentPlaylistId)
      .pipe(take(1))
      .subscribe((playlist: Playlist) => {
        this.playlist = playlist;

        // Get all tracks from the playlist
        if (this.playlist.trackIds && this.playlist.trackIds.length > 0) {
          this.playlist.trackIds.forEach((trackId) => {
            this.trackService
              .getTrack(trackId)
              .pipe(take(1))
              .subscribe((track: Track) => {
                this.tracks.push(track);

                // Fill userMap with user data of the tracks
                if (!this.userMap[track.artistId]) {
                  this.userService
                    .getCustomer(track.artistId)
                    .pipe(take(1))
                    .subscribe((user: User) => {
                      this.userMap[track.artistId] = user;
                    });
                }

                // Fill albumMap with album data of the tracks
                if (!this.albumMap[track.albumId]) {
                  this.albumService
                    .getAlbum(track.albumId)
                    .pipe(take(1))
                    .subscribe((album: Album) => {
                      this.albumMap[track.albumId] = album;
                      // Manually trigger change detection
                      this.cdr.detectChanges();
                    });
                }
              });
          });
        }
      });
  }

  playPlaylist() {
    const mockTracks: PlayerTrack[] = this.tracks.map((track) => {
      const artist = this.userMap[track.artistId];
      const albumCoverUrl = this.albumMap[track.albumId]?.coverPath;

      return {
        id: track.id,
        title: track.title,
        trackPath: track.trackPath,
        artist,
        albumCoverUrl,
      };
    });

    this.audioService.setPlaylist(mockTracks);
  }

  getArtistName(artistId: string) {
    return this.userMap[artistId]?.username;
  }

  getAlbumCover(albumId: string) {
    return this.albumMap[albumId]?.coverPath;
  }

  getAlbumName(albumId: string) {
    return this.albumMap[albumId]?.title;
  }
}
