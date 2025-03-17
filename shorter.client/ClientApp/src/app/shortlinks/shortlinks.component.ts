import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DomSanitizer, SafeUrl } from '@angular/platform-browser';
import { ApiService } from '../api.service';

export interface Shortlink {
  id: number;
  longUrl: string;
  shortUrl: string;
  totalClicks: number;
  dateCreated: string;
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
  loading: boolean = false;
  error: string | null = null;

  constructor(private apiService: ApiService) {}

  ngOnInit(): void {
    this.loadShortlinks();
  }

  loadShortlinks(): void {
    this.loading = true;
    this.error = null;
    this.apiService.getUrls().subscribe({
      next: (data) => {
        console.log('Fetched shortlinks:', data);
        this.shortlinks = data;
        this.loading = false;
      },
      error: (err) => {
        console.error('Error fetching shortlinks:', err);
        this.error = 'Failed to load short links. Please try again.';
        this.loading = false;
      }
    });
  }

  deleteShortlink(id: number): void {
    if (confirm('Are you sure you want to delete this shortlink?')) {
      this.apiService.deleteUrl(id).subscribe({
        next: () => {
          // remove the deleted link from the list
          this.shortlinks = this.shortlinks.filter(link => link.id !== id);
        },
        error: (err) => {
          console.error('Error deleting shortlink:', err);
        }
      });
    }
  }
}
