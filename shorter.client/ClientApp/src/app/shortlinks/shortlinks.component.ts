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

  constructor(private apiService: ApiService, private sanitizer: DomSanitizer) {}

  ngOnInit(): void {
    this.loadShortlinks();
  }

  loadShortlinks(): void {
    this.apiService.getUrls().subscribe({
      next: (data) => {
        console.log("Fetched shortlinks:", data);
        this.shortlinks = data;
      },
      error: (err) => {
        console.error("Error fetching shortlinks:", err);
      }
    });
  }

  deleteShortlink(id: number): void {
    if (confirm("Are you sure you want to delete this shortlink?")) {
      this.apiService.deleteUrl(id).subscribe({
        next: () => { this.shortlinks = this.shortlinks.filter(link => link.id !== id); },
        error: (err) => { console.error("Error deleting shortlink:", err); }
      });
    }
  }

  getRedirectUrl(shortUrl: string): SafeUrl {
    const fullUrl = `https://localhost:7161/api/Url/RedirectShortUrl?shortUrl=${shortUrl}`;
    return this.sanitizer.bypassSecurityTrustUrl(fullUrl);
  }
}
