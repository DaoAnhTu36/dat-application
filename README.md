# dat-Application
million dollars application



  currentPage: number = 0;
  totalPage: number = 0;
  pageNumber: number[] = [];
  
  OnPage(index: number, type: string) {
    if (type === 'previous') {
      this.currentPage -= 1;
      this.list(this.currentPage);
    } else if (type === 'next') {
      this.currentPage += 1;
      this.list(this.currentPage);
    } else {
      this.currentPage = index;
      this.list(this.currentPage);
    }
  }
  
        this.pageNumber = [];
        this.currentPage = res.pageInfo?.currentPage ?? 0;
        this.totalPage = res.pageInfo?.totalPage ?? 0;
        for (let index = 0; index < this.totalPage; index++) {
          this.pageNumber.push(index);
        }