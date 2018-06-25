export class App {

  public appId: number;
  public name: string;
  public developer: string;
  public description: string;
  public price: number;
  public thumbUrl: string;

  static transform<T>(data: any): T {
      if (data.constructor === Array) {
          return data.map(App.build) as T;
      }

      return App.build<T>(data);
  }

  static build<T>(data: any): T {
      const app = new App();

      Object.assign(app, data);

      return <any>app as T;
  }

}
