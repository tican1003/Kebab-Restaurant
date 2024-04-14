import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { map } from 'rxjs';
import { Item } from 'src/app/models/item';
import { Order } from 'src/app/models/order';
import { User } from 'src/app/models/user';
import { AccountService } from 'src/app/services/account.service';
import { FoodService } from 'src/app/services/food.service';
import { OrderService } from 'src/app/services/order.service';

@Component({
  selector: 'app-order-detail',
  templateUrl: './order-detail.component.html',
  styleUrls: ['./order-detail.component.css'],
})
export class OrderDetailComponent implements OnInit {
  orderId: Number | null = null;
  order: Order | undefined;
  items: Item[] | undefined;
  total: Number = 0;
  totalQuantity: Number = 0;
  constructor(
    private orderService: OrderService,
    public accountService: AccountService,
    private foodService: FoodService,
    private router: Router,
    private toastr: ToastrService
  ) {}
  ngOnInit(): void {
    this.loadOrderDetail();
    this.loadItems();
  }

  loadOrderDetail() {
    this.orderId = Number(localStorage.getItem('orderId'));
    this.orderService.getOrder(this.orderId).subscribe({
      next: (order) => {
        order.id = Number(localStorage.getItem('orderId'));
        this.order = order;
      },
    });
  }

  loadItems() {
    if (this.orderId) {
      this.foodService.getItemsByOrderId(this.orderId).subscribe({
        next: (items) => {
          this.items = items;
          for (let item of items) {
            this.total =
              Number(this.total) + Number(item.price) * Number(item.quantity);
            this.totalQuantity =
              Number(this.totalQuantity) + Number(item.quantity);
          }
        },
      });
    }
  }

  addItem() {
    this.router.navigateByUrl('/menu');
  }

  plusItem(menuId: Number) {
    console.log(this.orderId);
    this.foodService.plusItem(Number(this.orderId), menuId).subscribe({
      next: (_) => {
        this.toastr.success('Plus quantity successfully!', 'Success'),
          window.location.reload();
      },
      error: (err) => this.toastr.error('Failed to plus quantity!', 'Error'),
    });
  }

  minusItem(menuId: Number) {
    this.foodService.minusItem(Number(this.orderId), menuId).subscribe({
      next: (_) => {
        this.toastr.success('Minus quantity successfully!', 'Success'),
          window.location.reload();
      },
      error: (err) => this.toastr.error('Failed to minus quantity!', 'Error'),
    });
  }
  invoice() {
    this.foodService.deactiveItem(Number(this.orderId)).subscribe({
      next: (_) => {
        this.toastr.success('Invoice successfully!', 'Success'),
          window.location.reload();
      },
      error: (err) => this.toastr.error('Failed to invoice!', 'Error'),
    });
  }
}
