import { Component } from '@angular/core';
import { RouterLink } from '@angular/router';

import { Navbar } from '../../shared/components/navbar/navbar';
import { MovieCard } from '../movies/movie-card/movie-card';

@Component({
  selector: 'app-home',
  imports: [Navbar, RouterLink, MovieCard],
  templateUrl: './home.html',
  styleUrl: './home.scss'
})
export class Home {

  movies = [
    {
      id: 1,
      title: 'Leo',
      genre: 'Action / Thriller',
      rating: 8.2,
      poster: 'https://image.tmdb.org/t/p/w500/2V6U5lV3g1Jq0Wq7QJ5q3m7Qv9A.jpg'
    },
    {
      id: 2,
      title: 'Vikram',
      genre: 'Action / Crime',
      rating: 8.4,
      poster: 'https://image.tmdb.org/t/p/w500/7B4z2j9Y5Q6x8Z1m3P0k4v6n8Q.jpg'
    },
    {
      id: 3,
      title: 'Jailer',
      genre: 'Action / Comedy',
      rating: 7.8,
      poster: 'https://image.tmdb.org/t/p/w500/9J7k3m5P2Q6x8Z1v4B0n5Y3c.jpg'
    },
    {
      id: 4,
      title: 'Master',
      genre: 'Action / Drama',
      rating: 7.5,
      poster: 'https://image.tmdb.org/t/p/w500/6K8m2P4Q9x1Z5v3B7n0Y6c.jpg'
    }
  ];

}