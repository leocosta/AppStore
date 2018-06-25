import { Injectable } from '@angular/core';

import { StorageService } from './storage.service';
import { User } from '../../layout/user/shared/user.model';

const USER_ID = 'UserId';

@Injectable()
export class UserSession {
    private _userId: number;

    private _user: User;

    constructor(private storageService: StorageService) { }

    get userId(): number {
        if (!this._userId) {
            this._userId = +this.storageService.getItem(USER_ID);
        }
        return this._userId;
    }

    get user(): User {
        return this._user;
    }

    get isLoggedIn(): boolean {
        return !!this.userId;
    }

    create(user: User) {
        this.setIdUsuario(user.userId);
        this._user = user;
    }

    destroy(): void {
        this.storageService.removeItem(USER_ID);
        this._userId = null;
    }

    private setIdUsuario(value: number) {
        this.storageService.setItem(USER_ID, value);
        this._userId = value;
    }
}
