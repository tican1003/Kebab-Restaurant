import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Item } from '../models/item';
import { environment } from 'src/environments/environment.development';

@Injectable({
  providedIn: 'root',
})
export class FoodService {
  item: Item | undefined;
  baseUrl = environment.apiUrl;
  constructor(private http: HttpClient) {}

  getItems() {
    return this.http.get<Item[]>(this.baseUrl + 'item');
  }
  getItemsByOrderId(id: Number) {
    return this.http.get<Item[]>(this.baseUrl + `item/order/${id}`);
  }
  addItem(item: Item) {
    return this.http.post<Item>(this.baseUrl + 'item', item);
  }
  checkExist(menuId: Number) {
    var menu = this.http.get<Item>(this.baseUrl + `item/exist/${menuId}`);
    return menu;
  }
  checkExistOrder(orderId: Number) {
    var order = this.http.get<Item>(
      this.baseUrl + `item/exist/order/${orderId}`
    );
    return order;
  }
  plusItem(orderId: Number, menuId: Number) {
    return this.http.post<Item>(this.baseUrl + `item/plus`, {
      orderId,
      menuId,
    });
  }
  minusItem(orderId: Number, menuId: Number) {
    return this.http.post<Item>(this.baseUrl + `item/minus`, {
      orderId,
      menuId,
    });
  }
  deactiveItem(orderId: Number) {
    return this.http.get<Item>(this.baseUrl + `order/deactive/${orderId}`);
  }
}
