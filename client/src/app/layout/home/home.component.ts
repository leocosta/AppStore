import { Component, OnInit } from '@angular/core';
import { routerTransition } from '../../router.animations';
import { App, AppService } from '../apps/shared';
import { Observable } from 'rxjs';

@Component({
    selector: 'app-home',
    templateUrl: './home.component.html',
    styleUrls: ['./home.component.scss'],
    animations: [ routerTransition() ],
    providers: [ AppService ]
})
export class HomeComponent implements OnInit {
    public apps: Observable<App[]>;

    constructor(private appService: AppService) {
        this.apps = this.appService.getAll();
    }

    ngOnInit() {}
}
