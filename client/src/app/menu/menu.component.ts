import { Component, OnInit } from '@angular/core';
import { ItemsService } from '../services/items.service';
import { Menu } from '../models/menu';
import { FoodService } from '../services/food.service';
import { Item } from '../models/item';
import { OrderService } from '../services/order.service';
import { Order } from '../models/order';
import { BehaviorSubject } from 'rxjs';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-menu',
  templateUrl: './menu.component.html',
  styleUrls: ['./menu.component.css'],
})
export class MenuComponent implements OnInit {
  title = 'Menu';
  menu: Menu[] | undefined;
  currentMenu: Menu | undefined;
  item: Item | undefined;
  exist: boolean = false;
  constructor(
    private itemsService: ItemsService,
    private foodService: FoodService,
    private toastr: ToastrService
  ) {}
  ngOnInit(): void {
    this.loadMenu();
  }
  loadMenu() {
    this.itemsService.getMenu().subscribe({
      next: (menu) => {
        this.menu = menu;
      },
    });
  }
  addItemHandler(menuId: Number) {
    this.itemsService.getMenuById(menuId).subscribe({
      next: (itemMenu) => {
        this.currentMenu = itemMenu;
        let copy = JSON.parse(JSON.stringify(this.currentMenu));
        this.item = Object.assign(
          {},
          {
            ...copy,
            isSuccess: false,
            orderId: localStorage.getItem('orderId'),
            menuId: menuId,
            quantity: 1,
          }
        );
        if (this.item) {
          let orderId = Number(localStorage.getItem('orderId'));
          this.foodService.checkExist(menuId).subscribe({
            next: (checkMenu) => {
              console.log('Menu' + checkMenu);
              if (checkMenu) {
                this.foodService.checkExistOrder(orderId).subscribe({
                  next: (checkOrder) => {
                    console.log('Order' + checkOrder);
                    if (checkOrder) {
                      if (this.item) this.plusItem(orderId, menuId);
                    } else {
                      if (this.item) this.addItem(this.item);
                    }
                  },
                });
              } else {
                if (this.item) this.addItem(this.item);
              }
            },
          });
        }
      },
    });
  }

  addItem(item: Item) {
    this.foodService.addItem(item).subscribe({
      next: (_) =>
        this.toastr.success('Add food to order successfully!', 'Success'),
      error: (err) => this.toastr.error(err.error),
    });
  }

  plusItem(orderId: Number, menuId: Number) {
    this.foodService.plusItem(orderId, menuId).subscribe({
      next: (_) => {
        return this.toastr.success(
          'Add food to order successfully!',
          'Success'
        );
      },
      error: (err) => this.toastr.error(err.error),
    });

    if (this.item) this.item.quantity = Number(this.item.quantity) + 1;
  }
}
