import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { routerTransition } from '../router.animations';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { CustomValidators } from '../shared/validators';
import { UserSession } from '../shared/services';
import { Credential, AuthenticationService } from './shared';

@Component({
    selector: 'app-login',
    templateUrl: './login.component.html',
    styleUrls: ['./login.component.scss'],
    animations: [ routerTransition() ],
    providers: [ AuthenticationService ]
})
export class LoginComponent implements OnInit {
    constructor(public router: Router, 
        private userSession: UserSession, 
        private authenticationService: AuthenticationService) {}

    loginForm: FormGroup;
    credential: Credential = new Credential;    

    ngOnInit() {
        this.configureForm();
    }

    login() {

        if (!this.loginForm.valid) {
            alert('Você deve preencher o formulário corretamente.');
            return;
        }

        this.authenticationService.post(this.credential)
          .subscribe((response: any) => {
            this.userSession.create(response.user);
            this.router.navigate(['home']);
          }, () => alert('Erro ao efetuar login.'));
    }

    private configureForm(): void {

        this.loginForm = new FormGroup({
            'email': new FormControl(null, Validators.compose([Validators.required, CustomValidators.email])),
            'password': new FormControl(null, Validators.compose([Validators.required]))
        });
    }
}
