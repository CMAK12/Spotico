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
    { title: '92', artist: 'Valentin Strykalo', cover: 'https://images.genius.com/8dc46c831d67172c02afe05d921747dd.1000x1000x1.png' },
    { title: '92', artist: 'Valentin Strykalo', cover: 'https://images.genius.com/8dc46c831d67172c02afe05d921747dd.1000x1000x1.png' },
    { title: '92', artist: 'Valentin Strykalo', cover: 'https://images.genius.com/8dc46c831d67172c02afe05d921747dd.1000x1000x1.png' },
    { title: '92', artist: 'Valentin Strykalo', cover: 'https://images.genius.com/8dc46c831d67172c02afe05d921747dd.1000x1000x1.png' },
    { title: '92', artist: 'Valentin Strykalo', cover: 'https://images.genius.com/8dc46c831d67172c02afe05d921747dd.1000x1000x1.png' },
    { title: '92', artist: 'Valentin Strykalo', cover: 'https://images.genius.com/8dc46c831d67172c02afe05d921747dd.1000x1000x1.png' },
    { title: '92', artist: 'Valentin Strykalo', cover: 'https://images.genius.com/8dc46c831d67172c02afe05d921747dd.1000x1000x1.png' },
    { title: '92', artist: 'Valentin Strykalo', cover: 'https://images.genius.com/8dc46c831d67172c02afe05d921747dd.1000x1000x1.png' },
    { title: '92', artist: 'Valentin Strykalo', cover: 'https://images.genius.com/8dc46c831d67172c02afe05d921747dd.1000x1000x1.png' },
    { title: '92', artist: 'Valentin Strykalo', cover: 'https://images.genius.com/8dc46c831d67172c02afe05d921747dd.1000x1000x1.png' },
    { title: '92', artist: 'Valentin Strykalo', cover: 'https://images.genius.com/8dc46c831d67172c02afe05d921747dd.1000x1000x1.png' },
    { title: '92', artist: 'Valentin Strykalo', cover: 'https://images.genius.com/8dc46c831d67172c02afe05d921747dd.1000x1000x1.png' },
  ]

  topArtists = [
    { name: 'Tyler, The Creator', image: 'https://i.scdn.co/image/ab6761610000e5ebdfa2b0c7544a772042a12e52'},
    { name: 'Kanye West', image: 'https://upload.wikimedia.org/wikipedia/commons/thumb/1/11/Kanye_West_at_the_2009_Tribeca_Film_Festival.jpg/1200px-Kanye_West_at_the_2009_Tribeca_Film_Festival.jpg' },
    { name: 'Alex G', image: 'https://thefader-res.cloudinary.com/private_images/w_1440,c_limit,f_auto,q_auto:best/DSC03149_cwagtf/alex-g-is-trying-to-tell-you-something.jpg' },
    { name: 'Mac DeMarco', image: 'https://images-prod.dazeddigital.com/960/azure/dazed-prod/1330/7/1337869.jpg' },
    { name: 'Dream, Ivory', image: 'https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQVcgY547FHIRp8IwgATcTbUe98Vx8oczkV0Q&s' }
  ]
}
