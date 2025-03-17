import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, CommonModule],
  template: `
    <header>
      <nav class="navbar navbar-expand-sm navbar-light bg-white border-bottom box-shadow mb-3">
        <div class="container-fluid">
          <a class="navbar-brand" routerLink="/home">shorter.</a>
          <button class="navbar-toggler" type="button" data-bs-toggle="collapse"
                  data-bs-target=".navbar-collapse" aria-controls="navbarSupportedContent"
                  aria-expanded="false" aria-label="Toggle navigation">
            <span class="navbar-toggler-icon"></span>
          </button>
          <div class="navbar-collapse collapse d-sm-inline-flex justify-content-between">
            <ul class="navbar-nav flex-grow-1">
              <li class="nav-item">
                <a class="nav-link text-dark"
                   href="https://localhost:7161/">home</a>
              </li>
              <li class="nav-item">
                <a class="nav-link text-dark"
                   href="https://localhost:7161/Auth/Users">users</a>
              </li>
              <li class="nav-item">
                <a class="nav-link text-dark"
                   href="https://localhost:7161/Home/About">about</a>
              </li>
            </ul>
          </div>
          <div>
            <button class="btn btn-outline-danger" (click)="logout()">log out</button>
          </div>
        </div>
      </nav>
    </header>
    <main role="main" class="pb-3">
      <router-outlet></router-outlet>
    </main>
    <footer class="container">
      <div class="d-flex flex-wrap justify-content-between align-items-center py-3 my-3 border-top">
        <div class="col-md-4 d-flex align-items-center">
          <span class="mb-3 mb-md-0">
            <a class="text-muted" href="https://github.com/kkerosene">github</a>
          </span>
        </div>
      </div>
    </footer>
  `,
})
export class AppComponent {
  logout() {
    window.location.href = 'https://localhost:7161/Auth/Logout';
  }
}
