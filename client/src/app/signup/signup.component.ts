import { Component, OnInit } from '@angular/core';
import { routerTransition } from '../router.animations';
import { User, UserService, Gender } from '../layout/user/shared';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { CustomValidators } from '../shared/validators';
import { Router } from '@angular/router';

@Component({
    selector: 'app-signup',
    templateUrl: './signup.component.html',
    styleUrls: ['./signup.component.scss'],
    animations: [ routerTransition() ],
    providers: [ UserService ]
})
export class SignupComponent implements OnInit {

    signUpForm: FormGroup;
    user: User = new User();
    states: Array<string> = new Array<string>();
    gender = Gender;

    constructor(private router: Router, 
        private userService: UserService) {}

    ngOnInit() {
        this.configureForm();
        this.loadStates();
    }

    onDateSelection($event): void {
        this.user.birthDate = new Date($event.year, $event.month, $event.day);
    }

    register() {
        if (!this.signUpForm.valid) {
            alert('Você deve preencher o formulário corretamente.');
            return;
        }
        this.userService.post(this.user)
          .subscribe(() => {
            alert('Usuário cadastrado com sucesso!');
            this.router.navigate(['home']);
          },
          () => alert('Erro ao cadastrar.'));
    }

    private configureForm(): void {

        this.signUpForm = new FormGroup({
            'name': new FormControl(null, Validators.compose([Validators.required])),
            'SSN': new FormControl(null, Validators.compose([Validators.required, CustomValidators.SSN])),
            'gender': new FormControl(null, Validators.compose([Validators.required])),
            'birthDate': new FormControl(null, Validators.compose([Validators.required])),
            'email': new FormControl(null, Validators.compose([Validators.required, CustomValidators.email])),
            'password': new FormControl(null, Validators.compose([Validators.required])),
            'address.zipCode': new FormControl(null, Validators.compose([Validators.required, CustomValidators.zipCode])),
            'address.city': new FormControl(null, Validators.compose([Validators.required])),
            'address.state': new FormControl(null, Validators.compose([Validators.required])),
            'address.street': new FormControl(null, Validators.compose([Validators.required])),
            'address.number': new FormControl(null, Validators.compose([Validators.required])),
            'address.complement': new FormControl(null),
            'address.district': new FormControl(null, Validators.compose([Validators.required]))
        });
    }

    private loadStates(): void {
        this.states = `AC,AL,AM,AP,BA,CE,DF,
        ES,GO,MA,MG,MS,MT,PA,PB,PE,PI,PR,RJ,
        RN,RO,RR,RS,SC,SE,SP,TO`.split(',');
    }
}
