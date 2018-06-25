import { Injectable } from '@angular/core';

@Injectable()
export class StorageService {

    constructor() { }

    getItem(key): string {
        return window.localStorage.getItem(key);
    }

    setItem(key, value) {
        window.localStorage.setItem(key, value);
    }

    removeItem(key) {
        window.localStorage.removeItem(key);
    }

    clear() {
        window.localStorage.clear();
    }
}
