import { Component, Input, OnInit } from '@angular/core';
import { User } from '../models/user';
import { AccountService } from '../services/account.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css'],
})
export class HeaderComponent implements OnInit {
  user: User | undefined;
  constructor(public accountService: AccountService, private router: Router) {}
  ngOnInit(): void {}

  logout() {
    this.accountService.logout();
    this.router.navigateByUrl('/');
  }
}
