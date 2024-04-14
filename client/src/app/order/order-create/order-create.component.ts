import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { map, take } from 'rxjs';
import { AccountService } from 'src/app/services/account.service';
import { OrderService } from 'src/app/services/order.service';

@Component({
  selector: 'app-order-create',
  templateUrl: './order-create.component.html',
  styleUrls: ['./order-create.component.css'],
})
export class OrderCreateComponent implements OnInit {
  title = 'Create Order';
  model: any = {};
  timeIn: Date = new Date();
  constructor(
    private accountService: AccountService,
    private orderService: OrderService,
    private router: Router,
    private toastr: ToastrService
  ) {}
  ngOnInit(): void {}

  createOrder() {
    this.accountService.currentUser$.pipe(
      map((x) => (this.model.username = x?.username))
    );
    this.model.timeIn = new Date(Date.now());

    this.orderService.createOrder(this.model).subscribe({
      next: (_) => {
        this.toastr.success('Create order is successfully!');
        setTimeout(() => {
          this.router.navigateByUrl('/orders');
        }, 1500);
      },
      error: (err) => this.toastr.error(err.error),
    });
  }
}
