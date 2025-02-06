
NgForOf, CommonModule, FormsModule

  currentPage: number = 0;
  totalPage: number = 0;
  pageNumber: number[] = [];
  pageNumberSelect: number = 0;
  
  onAsyncData() {
    this.list();
  }

  onFilter(pageNumber: number = PageingReq.PAGE_NUMBER) {
    this._loadingService.show();
    let req = {
      pageNumber: pageNumber,
      pageSize: PageingReq.PAGE_SIZE,
    };
    this._warehouseService.goodsList(req).subscribe((res) => {
      this._loadingService.hide();
      if (
        res.metaData?.statusCode === StatusCodeApiResponse.SUCCESS &&
        res.isNormal
      ) {
        this.pageNumber = [];
        this.currentPage = res.pageInfo?.currentPage ?? 0;
        this.totalPage = res.pageInfo?.totalPage ?? 0;
        for (let index = 0; index < this.totalPage; index++) {
          this.pageNumber.push(index);
        }
        this.data = res.data?.list || [];
      } else {
      }
    });
  }

  onPage(index: number, type: string) {
    if (type === 'previous') {
      this.currentPage -= 1;
      this.onFilter(this.currentPage);
    } else if (type === 'next') {
      this.currentPage += 1;
      this.onFilter(this.currentPage);
    } else {
      this.currentPage = index;
      this.onFilter(this.currentPage);
    }
    this.pageNumberSelect = this.currentPage;
  }

  onPageSelect(event: any) {
    this.currentPage = event.target.value;
    this.onFilter(this.currentPage);
    this.pageNumberSelect = this.currentPage;
  }





      <tr>
        <td>
          <button type="button" class="btn btn-primary" (click)="onAsyncData()">
            <i class="fa-solid fa-rotate-right"></i>
          </button>
        </td>
        <td>
          <div class="paging-area">
            <button
              type="button"
              class="btn"
              [ngClass]="currentPage > 1 ? ['show'] : 'hide'"
              (click)="onPage(0, 'previous')"
            >
              <i class="fa fa-chevron-left" aria-hidden="true"></i>
            </button>
            <button
              type="button"
              class="btn"
              *ngFor="let item of pageNumber"
              (click)="onPage(item + 1, 'current')"
              [ngClass]="
                item + 1 == currentPage ? ['btn-primary', 'show'] : ['hide']
              "
            >
              {{ item + 1 }}
            </button>
            <button
              type="button"
              class="btn"
              (click)="onPage(0, 'next')"
              [ngClass]="currentPage < totalPage ? ['show'] : 'hide'"
            >
              <i class="fa fa-chevron-right" aria-hidden="true"></i>
            </button>
          </div>
        </td>
        <td>
          <select
            class="form-control"
            name=""
            id=""
            [(ngModel)]="pageNumberSelect"
            (change)="onPageSelect($event)"
          >
            <option *ngFor="let page of pageNumber" [value]="page + 1">
              {{ page + 1 }}
            </option>
          </select>
        </td>
        <td colspan="4"></td>
      </tr>