import { CommonModule } from '@angular/common';
import { Component, inject, OnInit } from '@angular/core';
import { PlaylistService } from '../../services/playlist.service';
import { ActivatedRoute } from '@angular/router';
import { Playlist } from '../../models/playlist.model';

@Component({
  selector: 'app-playlist',
  standalone: true,
  imports: [
      CommonModule
    ],
  templateUrl: './playlist.component.html',
  styleUrl: './playlist.component.scss',
})
export class PlaylistComponent implements OnInit {
  private playlistService = inject(PlaylistService);
  private route = inject(ActivatedRoute);

  playlist: Playlist;

  ngOnInit() {
    const id = this.route.snapshot.paramMap.get('id');
    this.playlistService.getPlaylist(id).subscribe((playlist: Playlist) =>
      this.playlist = playlist
    );
  }

  // This is a temporary mock data for the playlist
  tracks = [
    { image: "https://i.scdn.co/image/ab67616d00004851aa34e56c440a5e8790b524c3", title: 'Blondie', artist: 'Current Joys', album: 'Wild Heart', duration: '3:45' },
    { image: "https://i.scdn.co/image/ab67616d00004851ec5c5ec0da8942e4b03254f3", title: 'welcome and goodbye', artist: 'Dream, Ivory', album: 'welcome and goodbye', duration: '4:05' },
    { image: "https://i.scdn.co/image/ab67616d000048513b71c629076bb56d445a186b", title: 'Mis', artist: 'Alex G', album: 'Rules', duration: '3:20' },
    { image: "https://i.scdn.co/image/ab67616d00004851c6d914dd1a455fb8dfb2f16b", title: 'Things To Do', artist: 'Alex G', album: 'Race', duration: '2:37' },
    { image: "https://i.scdn.co/image/ab67616d00004851a5bdf2701f8db7a1a5adefe5", title: 'домой', artist: 'ssshhhiiittt!', album: 'Третья жизнь', duration: '3:21' },
    { image: "https://i.scdn.co/image/ab67616d000048513e7c5c6aec8c7e27037aeb8e", title: 'Сквозь непогоду', artist: 'lifo', album: 'Сквозь непогоду', duration: '3:15' }
  ];
}
