import { Injectable } from '@angular/core';
import { CanActivate } from '@angular/router';
import { Router } from '@angular/router';
import { UserSession } from '../services';
import { AuthenticationService } from '../../login/shared';
import { Observable, of } from 'rxjs';
import { map, catchError } from 'rxjs/operators';
import { User } from '../../layout/user/shared';

@Injectable()
export class AuthGuard implements CanActivate {
    constructor(private router: Router,
        private userSession: UserSession,
        private authenticationService: AuthenticationService) { }
        
        canActivate(): Observable<boolean> {
            
            return this.authenticationService.getMe()
            .pipe(map((user: User) => {
                this.userSession.create(user);
                return true;
            }),
            catchError(error => {
                if (error.status === 401) {
                    this.router.navigate(['/login']);
                }
                return of(false)
            }));
            
        }
    }
    