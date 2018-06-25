export class Address {

    public street: string;
    public number: string;
    public complement: string;
    public district: string;
    public city: string;
    public state: string;
    public zipCode: string;

    static transform<T>(data: any): T {
        if (data.constructor === Array) {
            return data.map(Address.build) as T;
        }

        return Address.build<T>(data);
    }

    static build<T>(data: any): T {
        const user = new Address();

        Object.assign(user, data);

        return <any>user as T;
    }
}
