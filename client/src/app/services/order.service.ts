import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment.development';
import { Order } from '../models/order';
import { BehaviorSubject, filter, map } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class OrderService {
  order: Order[] | undefined;
  baseUrl = environment.apiUrl;
  private currentOrderSource = new BehaviorSubject<Order | null>(null);
  currentOrder$ = this.currentOrderSource.asObservable();
  constructor(private http: HttpClient) {}

  createOrder(model: any) {
    return this.http.post<Order>(this.baseUrl + 'order', model);
  }

  loadOrders() {
    return this.http.get<Order[]>(this.baseUrl + 'order').pipe(
      map((order) => {
        this.order = order;
        return order;
      })
    );
  }

  getOrder(id: Number) {
    return this.http.get<Order>(this.baseUrl + `order/${id}`).pipe(
      map((response: Order) => {
        this.currentOrderSource.next(response);
        return response;
      })
    );
  }
}
