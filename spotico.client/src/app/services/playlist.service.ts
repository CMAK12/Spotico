import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Playlist } from '../models/playlist.model';
import { PlaylistDTO } from '../DTOs/playlist.dto';

@Injectable({
  providedIn: 'root'
})
export class PlaylistService {
  private baseUrl = 'http://localhost:5032/api/playlist';

  constructor(
    private http: HttpClient
  ) { }

  getPlaylists(): Observable<Playlist[]> {
    return this.http.get<Playlist[]>(this.baseUrl);
  }

  getPlaylist(id: string): Observable<Playlist> {
    return this.http.get<Playlist>(`${this.baseUrl}/${id}`);
  }

  createPlaylist(playlist: PlaylistDTO): Observable<Playlist> {
    return this.http.post<Playlist>(this.baseUrl, playlist);
  }

  updatePlaylist(playlist: Playlist): Observable<Playlist> {
    return this.http.put<Playlist>(this.baseUrl, playlist);
  }

  deletePlaylist(id: string): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/${id}`);
  }
}
