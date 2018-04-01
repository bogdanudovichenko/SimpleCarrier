import {Component, OnInit} from '@angular/core';
import {FormControl, FormGroup, Validators} from '@angular/forms';
import {AuthService} from '../services/auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  public loginForm: FormGroup = new FormGroup({
    userName: new FormControl('', [
      Validators.required,
      Validators.minLength(2),
      Validators.maxLength(20)
    ]),
    password: new FormControl('', [
      Validators.required,
      Validators.minLength(2),
      Validators.maxLength(20)
    ])
  });

  constructor(private authService: AuthService) {
  }

  ngOnInit() {

  }

  onSubmit() {
    console.log(this.loginForm);
    if (this.loginForm.invalid) {
      return;
    }

    const loginInfo = this.loginForm.value;

    this.authService.login(loginInfo.userName, loginInfo.password)
      .subscribe(() => alert('login success'));
  }
}
