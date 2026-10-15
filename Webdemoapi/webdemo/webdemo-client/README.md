# WebdemoClient

This project was generated using [Angular CLI](https://github.com/angular/angular-cli) version 22.2.2.

## Chạy Angular và API cùng lúc

Đảm bảo SQL Server đang chạy và migration đã được cập nhật. Từ thư mục
`webdemo-client`, chạy:

```bash
npm run start:all
```

Lệnh này khởi chạy API trên `https://localhost:5001` và Angular trên
`http://localhost:4200`, đồng thời dừng tiến trình còn lại nếu một tiến trình
bị dừng. Angular proxy chuyển tiếp các yêu cầu `/api` tới API.

Mở `http://localhost:4200/`, đăng ký tài khoản mới hoặc đăng nhập. Sau khi
đăng nhập thành công, ứng dụng chuyển tới danh sách hàng hóa. Các API hàng hóa
yêu cầu cookie đăng nhập.

## Chỉ chạy Angular

To start a local development server, run:

```bash
npm start
```

Once the server is running, open your browser and navigate to `http://localhost:4200/`. The application will automatically reload whenever you modify any of the source files.

## Code scaffolding

Angular CLI includes powerful code scaffolding tools. To generate a new component, run:

```bash
ng generate component component-name
```

For a complete list of available schematics (such as `components`, `directives`, or `pipes`), run:

```bash
ng generate --help
```

## Building

To build the project run:

```bash
ng build
```

This will compile your project and store the build artifacts in the `dist/` directory. By default, the production build optimizes your application for performance and speed.

## Running unit tests

To execute unit tests with the [Vitest](https://vitest.dev/) test runner, use the following command:

```bash
ng test
```

## Running end-to-end tests

For end-to-end (e2e) testing, run:

```bash
ng e2e
```

Angular CLI does not come with an end-to-end testing framework by default. You can choose one that suits your needs.

## Additional Resources

For more information on using the Angular CLI, including detailed command references, visit the [Angular CLI Overview and Command Reference](https://angular.dev/tools/cli) page.
