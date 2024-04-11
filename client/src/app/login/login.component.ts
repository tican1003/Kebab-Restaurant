import { Component, OnInit } from '@angular/core';
import { AccountService } from '../services/account.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
})
export class LoginComponent implements OnInit {
  title = 'Login';
  model: any = {};
  constructor(private accountService: AccountService, private router: Router) {}
  ngOnInit(): void {}

  login() {
    this.accountService.login(this.model).subscribe({
      next: (_) => this.router.navigateByUrl('/'),
    });
  }

  logout() {
    this.accountService.logout();
    this.router.navigateByUrl('/');
  }
}
