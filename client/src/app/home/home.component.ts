import { Component, OnInit } from '@angular/core';
import { ItemsService } from '../services/items.service';
import { Menu } from '../models/menu';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css'],
})
export class HomeComponent implements OnInit {
  title = 'Home';
  menu: Menu[] | undefined;

  constructor(private itemsService: ItemsService) {}
  ngOnInit(): void {
    this.loadMenu();
  }

  loadMenu() {
    this.itemsService.getMenu().subscribe({
      next: (menu) => {
        this.menu = menu;
      },
    });

    if (this.menu) {
      if (this.menu.length > 6) {
        this.menu = this.menu.slice(1, 6);
      }
    }
  }
}
