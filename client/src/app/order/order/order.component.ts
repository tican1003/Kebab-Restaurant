import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Order } from 'src/app/models/order';
import { OrderService } from 'src/app/services/order.service';

@Component({
  selector: 'app-order',
  templateUrl: './order.component.html',
  styleUrls: ['./order.component.css'],
})
export class OrderComponent implements OnInit {
  order: Order[] | undefined;
  constructor(private orderService: OrderService, private router: Router) {}
  ngOnInit(): void {
    this.loadOrders();
  }
  loadOrders() {
    this.orderService.loadOrders().subscribe({
      next: (order) => {
        order.sort(function (x, y) {
          return x === y ? 0 : x ? -1 : 1;
        });
        this.order = order;
      },
    });
  }
  detailOrder(orderId: Number) {
    localStorage.setItem('orderId', JSON.stringify(orderId));
    this.router.navigateByUrl(`/orders/detail`);
  }
}
