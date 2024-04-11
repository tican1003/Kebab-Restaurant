import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment.development';
import { Menu } from '../models/menu';
import { map } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class ItemsService {
  baseUrl = environment.apiUrl;
  inventoryApiUrl = environment.inventoryApiUrl;
  menu: Menu[] = [];

  constructor(private http: HttpClient) {}

  getMenu() {
    // return this.http.get<Menu[]>(this.baseUrl + 'menu').pipe(
    //   map((menu) => {
    //     this.menu = menu;
    //     return menu;
    //   })
    // );

    return this.http.get<Menu[]>(this.baseUrl + 'menu').pipe(
      map((menu) => {
        this.menu = menu;
        return menu;
      })
    );
  }
}
