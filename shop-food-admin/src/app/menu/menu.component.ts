import { CommonModule, NgFor, NgIf } from '@angular/common';
import { Component, SimpleChanges } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { CommonServiceService } from '../services/common-service.service';
import { LocalStorageServiceService } from '../services/local-storage-service.service';
import {
  NotificationModels,
  WarehouseService,
} from '../services/warehouse-service.service';
import { PageingReq } from '../commons/const/ConstStatusCode';
import * as jquery from 'jquery';

@Component({
  selector: 'app-menu',
  standalone: true,
  imports: [NgFor, CommonModule, NgIf],
  templateUrl: './menu.component.html',
  styleUrl: './menu.component.scss',
})
export class MenuComponent {
  is_active = false;
  isShowMenu = false;
  prefix = 'wh';
  prefixAuth = 'auth';
  data_menu: any[] = [
    {
      path: `wh`,
      display_name: 'CMS',
      list_child: [
        {
          path: `dashboard`,
          display_name: 'Trang chủ',
          list_child: [],
        },
        {
          path: `transaction`,
          display_name: 'Giao dịch',
          list_child: [
            {
              path: `transaction`,
              display_name: 'Nhập/xuất hàng hóa',
              list_child: [],
            },
            {
              path: `inventory`,
              display_name: 'Tồn kho',
              list_child: [],
            },
            {
              path: `import`,
              display_name: 'Nhập hàng',
              list_child: [],
            },
            {
              path: `export`,
              display_name: 'Xuất hàng',
              list_child: [],
            },
          ],
        },
        {
          path: `system`,
          display_name: 'Hệ thống',
          list_child: [
            {
              path: `product`,
              display_name: 'Hàng hóa',
              list_child: [],
            },
            {
              path: `supplier`,
              display_name: 'Nhà cung cấp',
              list_child: [],
            },
            {
              path: `unit`,
              display_name: 'Đơn vị tính',
              list_child: [],
            },
            {
              path: `warehouse`,
              display_name: 'Kho hàng',
              list_child: [],
            },
          ],
        },
      ],
    },
    {
      path: ``,
      display_name: 'Auth',
      list_child: [
        {
          path: `login`,
          display_name: 'Đăng nhập',
          list_child: [],
        },
        {
          path: `register`,
          display_name: 'Đăng ký',
          list_child: [],
        },
        {
          path: `logout`,
          display_name: 'Logout',
          list_child: [],
        },
      ],
    },
  ];
  data_notify: {
    title: string | null | undefined;
    body: string | null | undefined;
    time: Date;
  }[] = [];
  customerName = 'DaoAnhTu';
  isShowNotificationArea = false;
  data_menu_group: any[] = [];
  is_open: boolean = false;
  constructor(
    private route: Router,
    private _localStorage: LocalStorageServiceService,
    private readonly _warehouseService: WarehouseService,
    private readonly _activatedRoute: ActivatedRoute
  ) {}

  ngOnInit(): void {
    this.getListNotification();
    this.data_menu_group = this.getPaths(this.data_menu);
  }

  ngDoCheck(): void {
    this.isShowMenu = !this.route.url.includes(this.prefixAuth);
    if (this.isShowMenu) {
      const dataCustomer = JSON.parse(
        this._localStorage.getData('CustomerInfor') ?? '{}'
      );
      this.customerName = dataCustomer.name;
    }
  }

  redirectMenu(path: string, event: any, list_child_length: number) {
    path = '/' + path;
    this.route.navigateByUrl(path);
    const clickedElement = event.target as HTMLElement;
    let element = $(clickedElement);
    $('.menu-area *').removeClass('active');
    if (element.parent().hasClass('active')) {
      element.parent().removeClass('active');
      element.parent().parent().children('ul').removeClass('show-element');
    } else {
      element.parent().addClass('active');
      element.parent().parent().children('ul').addClass('show-element');
    }
    if (element.parent().hasClass('active')) {
      console.log(1);
      element
        .parent()
        .parent()
        .each((i, e) => {
          $(e)
            .parent()
            .parent()
            .each((i, e1) => {
              $(e1).children('a').addClass('active');
              this.is_open = true;
            });
          $(e)
            .parent()
            .parent()
            .parent()
            .parent()
            .each((i, e2) => {
              $(e2).children('a').addClass('active');
              this.is_open = true;
            });
        });
    } else {
      console.log(2);
      element
        .parent()
        .parent()
        .each((i, e) => {
          $(e)
            .parent()
            .parent()
            .each((i, e1) => {
              $(e1).children('a').removeClass('active');
              this.is_open = false;
            });
          $(e)
            .parent()
            .parent()
            .parent()
            .parent()
            .each((i, e2) => {
              $(e2).children('a').removeClass('active');
              this.is_open = false;
            });
        });
    }
  }

  toggleNotify() {
    if (!this.isShowNotificationArea) {
      this._warehouseService
        .notificationByUserId({
          pageNumber: PageingReq.PAGE_NUMBER,
          pageSize: PageingReq.PAGE_SIZE,
        })
        .subscribe((res) => {
          this.data_notify =
            res.data?.list?.map((item) => {
              return {
                title: item.title,
                body: item.body,
                time: item.createdDate,
              };
            }) ?? [];
          this.isShowNotificationArea = !this.isShowNotificationArea;
        });
    } else {
      this.isShowNotificationArea = !this.isShowNotificationArea;
    }
  }

  getListNotification() {}

  getPaths(data: any[], parentPath = '') {
    let result: any[] = [];
    data.forEach((item) => {
      const currentPath = `${parentPath}/${item.path}`;
      if (item.list_child && item.list_child.length > 0) {
        result.push(...this.getPaths(item.list_child, currentPath));
      } else {
        result.push(currentPath);
      }
    });
    return result;
  }
}
