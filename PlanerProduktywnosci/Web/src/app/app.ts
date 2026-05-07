import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet], // To musi tu być!
  templateUrl: './app.html',
  styleUrl: './app.scss'
})
export class App {
  title = 'Planer Produktywności';
}