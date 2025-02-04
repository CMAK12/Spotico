import { Injectable, signal, WritableSignal } from '@angular/core';
import { PlayerTrack } from '../DTOs/player-track.dto';

type AudioState = {
  isPlaying: boolean;
  duration: number;
  currentTime: number;
  volume: number;
  currentTrackIndex: number;
  playlist: PlayerTrack[];
};

const BASE_URL = 'http://localhost:5032';

@Injectable({
  providedIn: 'root',
})
export class AudioService {
  private readonly audio: HTMLAudioElement;
  private readonly playlist: string[];
  private readonly state: WritableSignal<AudioState>;
  private trackIndex: number;

  constructor() {
    this.playlist = [];
    this.trackIndex = 0;
    this.audio = new Audio(this.playlist[this.trackIndex]);

    this.state = signal({
      isPlaying: false,
      duration: this.audio.duration,
      currentTime: 0,
      volume: parseFloat(localStorage.getItem('volume')),
      currentTrackIndex: this.trackIndex,
      playlist: [],
    });

    // Update state when metadata is loaded
    this.audio.addEventListener('loadedmetadata', () => {
      this.state.update((state) => ({
        ...state,
        currentTime: this.audio.currentTime,
        duration: this.audio.duration,
      }));
    });

    // Update current time song in state when time is updated
    this.audio.addEventListener('timeupdate', () => {
      this.state.update((state) => ({
        ...state,
        currentTime: this.audio.currentTime,
      }));
    });
  }

  // Play the audio
  playAudio(): void {
    this.audio.volume = this.state().volume;
    this.audio.play();
    this.state.update((state) => ({
      ...state,
      isPlaying: true,
    }));
  }

  // Pause the audio
  pauseAudio(): void {
    if (this.audio) {
      this.audio.pause();
      this.state.update((state) => ({
        ...state,
        isPlaying: false,
      }));
    }
  }

  // Stop the audio and reset the current time
  stopAudio(): void {
    if (this.audio) {
      this.audio.pause();
      this.audio.currentTime = 0;
      this.state.update((state) => ({
        ...state,
        isPlaying: false,
      }));
    }
  }

  // Play the next song in the playlist
  nextSong(): void {
    this.trackIndex = (this.trackIndex + 1) % this.playlist.length;
    this.state.update((state) => ({
      ...state,
      currentTrackIndex: this.trackIndex,
      currentTime: 0,
    }));
    this.audio.src = this.playlist[this.trackIndex];
    this.audio.currentTime = 0; // Reset currentTime
    this.audio.load();
    this.playAudio();
  }

  // Play the previous song in the playlist
  previousSong(): void {
    this.trackIndex =
      (this.trackIndex - 1 + this.playlist.length) % this.playlist.length;
    this.state.update((state) => ({
      ...state,
      currentTrackIndex: this.trackIndex,
      currentTime: 0,
    }));
    this.audio.src = this.playlist[this.trackIndex];
    this.audio.currentTime = 0; // Reset currentTime
    this.audio.load();
    this.playAudio();
  }

  // Set the playlist
  setPlaylist(playlist: PlayerTrack[]): void {
    this.trackIndex = 0; // Reset the track index
    this.playlist.length = 0; // Clear the existing playlist
    this.state.update((state) => ({
      ...state,
      currentTrackIndex: this.trackIndex,
      playlist: playlist,
    }));
    playlist.forEach((track) =>
      this.playlist.push(`${BASE_URL}${track.trackPath}`),
    ); // Add the track link to the playlist
    console.log(this.state().playlist);
    this.audio.src = this.playlist[this.trackIndex];
    this.audio.load();
    this.playAudio();
  }

  // Change the volume of the audio
  setVolume(volume: number): void {
    volume /= 100; // Convert to a 0.0 to 1.0 scale
    if (volume >= 0 && volume <= 1) {
      this.audio.volume = volume;
      this.state.update((state) => ({
        ...state,
        volume: volume,
      }));
      localStorage.setItem('volume', volume.toString()); // Save the volume value in local storage
    } else {
      console.error('Volume must be between 0.0 and 1.0');
    }
  }

  // Mute the audio
  muteSong(): void {
    if (this.audio.volume === 0) {
      const savedVolume = parseFloat(localStorage.getItem('volume'));
      this.audio.volume = savedVolume;
      this.state.update((state) => ({
        ...state,
        volume: savedVolume,
      }));
    } else {
      this.audio.volume = 0;
      this.state.update((state) => ({
        ...state,
        volume: 0,
      }));
    }
  }

  // Change the current time of the audio
  seek(duration: number): void {
    this.audio.currentTime = duration;
    this.state.update((state) => ({
      ...state,
      currentTime: duration,
    }));
  }

  get getState() {
    return this.state;
  }
}
