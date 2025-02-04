import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Track } from '../models/track.model';
import { TrackDTO } from '../DTOs/track.dto';

const API_URL = 'http://localhost:5032/api/track';

@Injectable({
  providedIn: 'root',
})
export class TrackService {
  constructor(private http: HttpClient) {}

  getTracks(): Observable<Track[]> {
    return this.http.get<Track[]>(API_URL);
  }

  getTrack(id: string): Observable<Track> {
    return this.http.get<Track>(`${API_URL}/${id}`);
  }

  createTrack(track: TrackDTO): Observable<Track> {
    const formData = new FormData();

    for (const key in track)
      if (track.hasOwnProperty(key)) formData.append(key, (track as any)[key]);

    return this.http.post<Track>(API_URL, formData);
  }

  updateTrack(track: Track): Observable<Track> {
    return this.http.put<Track>(API_URL, track);
  }

  deleteTrack(id: string): Observable<void> {
    return this.http.delete<void>(`${API_URL}/${id}`);
  }
}
