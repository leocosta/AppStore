import { Gender } from './gender.enum';
import { Address } from './address.model';

export class User {

    public userId: number;
    public name: string;
    public ssn: string;
    public birthDate: Date;
    public email: string;
    public password: string;
    public gender: Gender = Gender.Male;
    public address: Address = new Address;

    static transform<T>(data: any): T {
        if (data.constructor === Array) {
            return data.map(User.build) as T;
        }

        return User.build<T>(data);
    }

    static build<T>(data: any): T {
        const user = new User();

        Object.assign(user, data);

        return <any>user as T;
    }

    get firstName(): string {
        return this.name.split(' ')[0];
    }

    isMale(): boolean {
        return this.gender === Gender.Male;
    }

    isFemale(): boolean {
        return this.gender === Gender.Female;
    }

}
