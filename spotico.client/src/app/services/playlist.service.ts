import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Playlist } from '../models/playlist.model';
import { PlaylistDTO } from '../DTOs/playlist.dto';

const API_URL = 'http://localhost:5032/api/playlist';

@Injectable({
  providedIn: 'root',
})
export class PlaylistService {
  constructor(private http: HttpClient) {}

  getPlaylists(): Observable<Playlist[]> {
    return this.http.get<Playlist[]>(API_URL);
  }

  getPlaylist(id: string): Observable<Playlist> {
    return this.http.get<Playlist>(`${API_URL}/${id}`);
  }

  createPlaylist(playlist: PlaylistDTO): Observable<Playlist> {
    return this.http.post<Playlist>(API_URL, playlist);
  }

  updatePlaylist(playlist: Playlist): Observable<Playlist> {
    return this.http.put<Playlist>(API_URL, playlist);
  }

  deletePlaylist(id: string): Observable<void> {
    return this.http.delete<void>(`${API_URL}/${id}`);
  }
}
