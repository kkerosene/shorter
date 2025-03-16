import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-redirect',
  standalone: true,
  template: `<p>Redirecting...</p>`
})
export class RedirectComponent implements OnInit {
  constructor(private route: ActivatedRoute) { }

  ngOnInit(): void {
    this.route.paramMap.subscribe(params => {
      const shortUrl = params.get('shortUrl');
      if (shortUrl) {
        // Build the API redirect URL.
        const apiRedirectUrl = `https://localhost:7161/api/Url/RedirectShortUrl?shortUrl=${shortUrl}`;
        // Trigger a full page navigation.
        window.location.href = apiRedirectUrl;
      }
    });
  }
}
