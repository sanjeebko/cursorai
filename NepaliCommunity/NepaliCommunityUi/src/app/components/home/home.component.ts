import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [CommonModule, RouterLink],
  template: `
    <div class="hero-section">
      <div class="hero-content">
        <h2>Welcome to Nepali Community</h2>
        <p>Connect with fellow Nepalis around the world. Share stories, organize events, and celebrate our rich culture together.</p>
        <div class="hero-buttons">
          <button class="btn btn-primary" routerLink="/register">Join Community</button>
          <button class="btn btn-secondary" routerLink="/posts">Learn More</button>
        </div>
      </div>
    </div>

    <div class="features-section">
      <div class="container">
        <h3>Community Features</h3>
        <div class="features-grid">
          <div class="feature-card">
            <div class="feature-icon">👥</div>
            <h4>Connect</h4>
            <p>Meet and connect with Nepalis from around the globe</p>
          </div>
          <div class="feature-card">
            <div class="feature-icon">📅</div>
            <h4>Events</h4>
            <p>Discover and organize community events and gatherings</p>
          </div>
          <div class="feature-card">
            <div class="feature-icon">🎭</div>
            <h4>Culture</h4>
            <p>Share and celebrate our rich Nepali culture and traditions</p>
          </div>
          <div class="feature-card">
            <div class="feature-icon">💬</div>
            <h4>Discussions</h4>
            <p>Engage in meaningful conversations and discussions</p>
          </div>
        </div>
      </div>
    </div>

    <div class="recent-posts-section">
      <div class="container">
        <h3>Recent Community Posts</h3>
        <div class="posts-grid">
          <div class="post-card">
            <h5>Dashain Celebration 2024</h5>
            <p>Join us for the biggest Dashain celebration in the city! Traditional food, music, and dance...</p>
            <div class="post-meta">
              <span>By Ram Sharma</span>
              <span>2 days ago</span>
            </div>
          </div>
          <div class="post-card">
            <h5>Nepali Language Classes</h5>
            <p>Starting new Nepali language classes for children. Help your kids stay connected to our roots...</p>
            <div class="post-meta">
              <span>By Sita Gurung</span>
              <span>5 days ago</span>
            </div>
          </div>
          <div class="post-card">
            <h5>Cultural Exchange Program</h5>
            <p>Exciting opportunity for cultural exchange with other South Asian communities...</p>
            <div class="post-meta">
              <span>By Krishna Thapa</span>
              <span>1 week ago</span>
            </div>
          </div>
        </div>
      </div>
    </div>
  `,
  styles: [`
    /* Hero Section */
    .hero-section {
      background: linear-gradient(135deg, #007bff, #0056b3);
      color: white;
      padding: 5rem 0;
      text-align: center;
    }

    .hero-content {
      max-width: 800px;
      margin: 0 auto;
      padding: 0 2rem;
    }

    .hero-content h2 {
      font-size: 3rem;
      font-weight: 700;
      margin-bottom: 1.5rem;
      line-height: 1.2;
    }

    .hero-content p {
      font-size: 1.25rem;
      margin-bottom: 2.5rem;
      opacity: 0.95;
      line-height: 1.6;
    }

    .hero-buttons {
      display: flex;
      gap: 1rem;
      justify-content: center;
      flex-wrap: wrap;
    }

    /* Buttons */
    .btn {
      padding: 0.75rem 2rem;
      border: none;
      border-radius: 0.5rem;
      font-size: 1rem;
      font-weight: 600;
      cursor: pointer;
      transition: all 0.3s ease;
      text-decoration: none;
      display: inline-block;
      text-align: center;
    }

    .btn-primary {
      background-color: #28a745;
      color: white;
    }

    .btn-primary:hover {
      background-color: #218838;
      transform: translateY(-2px);
      box-shadow: 0 4px 6px rgba(0,0,0,0.15);
    }

    .btn-secondary {
      background-color: transparent;
      color: white;
      border: 2px solid white;
    }

    .btn-secondary:hover {
      background-color: white;
      color: #007bff;
      transform: translateY(-2px);
    }

    /* Container */
    .container {
      max-width: 1200px;
      margin: 0 auto;
      padding: 0 2rem;
    }

    /* Features Section */
    .features-section {
      padding: 5rem 0;
      background-color: white;
    }

    .features-section h3 {
      text-align: center;
      font-size: 2.5rem;
      font-weight: 700;
      margin-bottom: 3rem;
      color: #2c3e50;
    }

    .features-grid {
      display: grid;
      grid-template-columns: repeat(auto-fit, minmax(280px, 1fr));
      gap: 2rem;
    }

    .feature-card {
      background: white;
      padding: 2.5rem 1.5rem;
      border-radius: 1rem;
      text-align: center;
      box-shadow: 0 2px 4px rgba(0,0,0,0.1);
      transition: all 0.3s ease;
      border: 1px solid #dee2e6;
    }

    .feature-card:hover {
      transform: translateY(-5px);
      box-shadow: 0 4px 6px rgba(0,0,0,0.15);
    }

    .feature-icon {
      font-size: 3rem;
      margin-bottom: 1rem;
    }

    .feature-card h4 {
      font-size: 1.5rem;
      font-weight: 600;
      margin-bottom: 1rem;
      color: #2c3e50;
    }

    .feature-card p {
      color: #6c757d;
      line-height: 1.6;
    }

    /* Recent Posts Section */
    .recent-posts-section {
      padding: 5rem 0;
      background-color: #f8f9fa;
    }

    .recent-posts-section h3 {
      text-align: center;
      font-size: 2.5rem;
      font-weight: 700;
      margin-bottom: 3rem;
      color: #2c3e50;
    }

    .posts-grid {
      display: grid;
      grid-template-columns: repeat(auto-fit, minmax(350px, 1fr));
      gap: 2rem;
    }

    .post-card {
      background: white;
      padding: 2rem;
      border-radius: 1rem;
      box-shadow: 0 2px 4px rgba(0,0,0,0.1);
      transition: all 0.3s ease;
      border: 1px solid #dee2e6;
    }

    .post-card:hover {
      transform: translateY(-3px);
      box-shadow: 0 4px 6px rgba(0,0,0,0.15);
    }

    .post-card h5 {
      font-size: 1.25rem;
      font-weight: 600;
      margin-bottom: 1rem;
      color: #2c3e50;
    }

    .post-card p {
      color: #6c757d;
      margin-bottom: 1.5rem;
      line-height: 1.6;
    }

    .post-meta {
      display: flex;
      justify-content: space-between;
      font-size: 0.875rem;
      color: #6c757d;
      border-top: 1px solid #dee2e6;
      padding-top: 1rem;
    }

    /* Responsive Design */
    @media (max-width: 768px) {
      .hero-content h2 {
        font-size: 2rem;
      }

      .hero-content p {
        font-size: 1.1rem;
      }

      .hero-buttons {
        flex-direction: column;
        align-items: center;
      }

      .btn {
        width: 100%;
        max-width: 250px;
      }

      .features-section h3,
      .recent-posts-section h3 {
        font-size: 2rem;
      }

      .features-grid,
      .posts-grid {
        grid-template-columns: 1fr;
      }

      .post-meta {
        flex-direction: column;
        gap: 0.5rem;
      }
    }

    @media (max-width: 480px) {
      .container {
        padding: 0 1rem;
      }

      .hero-section {
        padding: 3rem 0;
      }

      .features-section,
      .recent-posts-section {
        padding: 3rem 0;
      }

      .feature-card,
      .post-card {
        padding: 1.5rem;
      }
    }
  `]
})
export class HomeComponent {
} 