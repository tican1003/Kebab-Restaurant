import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
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
    const headerDict = {
      'Content-Type': 'application/json',
      Accept: 'application/json',
      'Access-Control-Allow-Headers': 'Content-Type',
      'ngrok-skip-browser-warning': '69420',
    };

    const requestOptions = {
      headers: new HttpHeaders(headerDict),
    };

    return this.http
      .get<Menu[]>(this.inventoryApiUrl + 'Menu', requestOptions)
      .pipe(
        map((menu) => {
          this.menu = menu;
          return menu;
        })
      );
  }

  getMenuById(menuId: Number) {
    // return this.http.get<Menu>(this.baseUrl + `menu/${menuId}`);
    const headerDict = {
      'Content-Type': 'application/json',
      Accept: 'application/json',
      'Access-Control-Allow-Headers': 'Content-Type',
      'ngrok-skip-browser-warning': '69420',
    };

    const requestOptions = {
      headers: new HttpHeaders(headerDict),
    };

    return this.http.get<Menu>(
      this.inventoryApiUrl + `Menu/${menuId}`,
      requestOptions
    );
  }
}
