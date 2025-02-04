import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Album } from '../models/album.model';
import { AlbumDTO } from '../DTOs/album.dto';

const API_URL = 'http://localhost:5032/api/album';

@Injectable({
  providedIn: 'root',
})
export class AlbumService {
  constructor(private http: HttpClient) {}

  getAlbums(): Observable<Album[]> {
    return this.http.get<Album[]>(API_URL);
  }

  getAlbum(id: string): Observable<Album> {
    return this.http.get<Album>(`${API_URL}/${id}`);
  }

  createAlbum(album: AlbumDTO): Observable<Album> {
    return this.http.post<Album>(API_URL, album);
  }

  updateAlbum(album: Album): Observable<Album> {
    return this.http.put<Album>(API_URL, album);
  }

  deleteAlbum(id: string): Observable<void> {
    return this.http.delete<void>(`${API_URL}/${id}`);
  }
}
