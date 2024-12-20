import { CommonModule, SlicePipe } from '@angular/common';
import { Component } from '@angular/core';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [
    CommonModule,
    SlicePipe
  ],
  templateUrl: './home.component.html',
  styleUrl: './home.component.scss'
})
export class HomeComponent {
  recentlyPlayed = [
    { title: 'Bohemian Rhapsody', artist: 'Queen', cover: 'https://ic.pics.livejournal.com/dubikvit/65747770/2455007/2455007_900.jpg' },
    { title: 'Stairway to Heaven', artist: 'Led Zeppelin', cover: 'https://i1.sndcdn.com/artworks-000127380203-93pa4d-t1080x1080.jpg' },
    { title: 'Imagine', artist: 'John Lennon', cover: 'https://cdn-images.dzcdn.net/images/cover/2675a9277dfabb74c32b7a3b2c9b0170/0x1900-000000-80-0-0.jpg' },
    { title: 'Smells Like Teen Spirit', artist: 'Nirvana', cover: 'https://i.scdn.co/image/ab67616d0000b273e175a19e530c898d167d39bf' },
    { title: '92', artist: 'Валентин Стрыкало', cover: 'https://images.genius.com/8dc46c831d67172c02afe05d921747dd.1000x1000x1.png' },
    { title: '92', artist: 'Валентин Стрыкало', cover: 'https://images.genius.com/8dc46c831d67172c02afe05d921747dd.1000x1000x1.png' },
    { title: '92', artist: 'Валентин Стрыкало', cover: 'https://images.genius.com/8dc46c831d67172c02afe05d921747dd.1000x1000x1.png' },
  ]
}
