import {
  AfterViewInit,
  ChangeDetectionStrategy,
  Component,
  ElementRef,
  HostListener,
  inject,
  OnDestroy,
  ViewChild,
} from '@angular/core';
import { AudioService } from '../../services/audio.service';
import { CommonModule } from '@angular/common';
import { TimeFormatPipe } from '../../pipes/time-format.pipe';

@Component({
  selector: 'app-player',
  standalone: true,
  imports: [CommonModule, TimeFormatPipe],
  templateUrl: './player.component.html',
  styleUrl: './player.component.scss',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class PlayerComponent implements OnDestroy {
  private readonly audioService = inject(AudioService);

  private interval;

  isMuted: boolean = false;
  state = this.audioService.getState;

  playAudio() {
    this.audioService.playAudio();

    this.interval = setInterval(() => {
      if (this.state().currentTime >= this.state().duration) {
        this.nextSong();
      }
    }, 1000);
  }

  pauseAudio() {
    this.audioService.pauseAudio();
  }

  stopAudio() {
    this.audioService.stopAudio();
  }

  setVolume(event: Event) {
    const volume = (event.target as HTMLInputElement).valueAsNumber;
    this.audioService.setVolume(volume);
  }

  seekDuration(event: Event) {
    this.audioService.seek((event.target as HTMLInputElement).valueAsNumber);
  }

  toggleMute() {
    this.isMuted = !this.isMuted;
    this.audioService.muteSong();
  }

  nextSong() {
    this.audioService.nextSong();
  }

  previousSong() {
    this.audioService.previousSong();
  }

  @HostListener('document:keydown', ['$event'])
  handleKeyboardEvent(event: KeyboardEvent) {
    if (this.state().playlist.length === 0) return; // Do nothing if playlist is empty

    event.preventDefault();

    switch (event.key) {
      case ' ':
        this.state().isPlaying ? this.pauseAudio() : this.playAudio();
        break;
      case 'ArrowRight':
        this.audioService.nextSong();
        break;
      case 'ArrowLeft':
        this.audioService.previousSong();
        break;
      case 'm':
      case 'M':
        this.toggleMute();
        break;
    }
  }

  ngOnDestroy(): void {
    clearInterval(this.interval);
    this.audioService.stopAudio();
  }
}
