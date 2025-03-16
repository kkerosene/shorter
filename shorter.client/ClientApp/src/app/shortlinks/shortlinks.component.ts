import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common'; // Needed for ngIf, ngFor, date pipe, etc.
import { ApiService } from '../api.service'; // Adjust the path as necessary

export interface Shortlink {
  id: number;
  longUrl: string;
  shortUrl: string;
  totalClicks: number;
  dateCreated: Date;
}

@Component({
  selector: 'app-shortlinks',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './shortlinks.component.html',
  styleUrls: ['./shortlinks.component.css']
})
export class ShortlinksComponent implements OnInit {
  shortlinks: Shortlink[] = [];

  constructor(private apiService: ApiService) { }

  ngOnInit(): void {
    this.loadShortlinks();
  }

  loadShortlinks(): void {
    this.apiService.getUrls().subscribe({
      next: (data) => {
        console.log('Fetched shortlinks:', data);
        this.shortlinks = data;
      },
      error: (err) => console.error('Error fetching shortlinks:', err)
    });
  }
}
