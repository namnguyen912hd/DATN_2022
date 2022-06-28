<template>
  <div>
    <div class="m-content" style="overflow: scroll;">
      <div class="m-title">Doanh thu 6 tháng gần nhất</div>
      <canvas id="myChart2" width="300" height="200"></canvas>
      <div class="m-title">Doanh thu 7 ngày gần nhất</div>
      <canvas id="myChart" width="300" height="200"></canvas>

    </div>

  </div>
</template>

<script>
import BaseRequest from "@/apis/baseRequest";
import Chart from 'chart.js/auto';
export default {
  components: {
  },
  data() {
    return {
      last6MonthsRenvenue: [],
      last7DaysRenvenue: [],
    };
  },
  watch: {
  },
  created() {
    this.getLast6MonthsRenvenue();
    this.getLast7DaysRenvenue();
  },
  mounted() {
    const ctx = document.getElementById('myChart');
    const myChart = new Chart(ctx, {
      type: 'line',
      data: {
        labels: this.getLast7Days(),
        datasets: [{
          label: 'Doanh thu 7 ngày gần nhất',
          data: this.last7DaysRenvenue,
          fill: false,
          borderColor: 'rgb(255, 99, 132)',
          tension: 0.1
        }]
      },
      options: {
        scales: {
          y: {
            beginAtZero: true
          }
        }
      }
    });
    const ctx2 = document.getElementById('myChart2');
    const myChart2 = new Chart(ctx2, {
      type: 'bar',
      data: {
        labels: this.getLast6Months(),
        datasets: [{
          label: 'Doanh thu 6 tháng gần nhất',
          data: this.last6MonthsRenvenue,
          backgroundColor: [
            'rgba(255, 99, 132, 0.2)',
            'rgba(54, 162, 235, 0.2)',
            'rgba(255, 206, 86, 0.2)',
            'rgba(75, 192, 192, 0.2)',
            'rgba(153, 102, 255, 0.2)',
            'rgba(255, 159, 64, 0.2)'
          ],
          borderColor: [
            'rgba(255, 99, 132, 1)',
            'rgba(54, 162, 235, 1)',
            'rgba(255, 206, 86, 1)',
            'rgba(75, 192, 192, 1)',
            'rgba(153, 102, 255, 1)',
            'rgba(255, 159, 64, 1)'
          ],
          borderWidth: 1
        }]
      },
      options: {
        scales: {
          y: {
            beginAtZero: true
          }
        }
      }
    });
    myChart
    myChart2
  },
  methods: {
    //#region hiển thị dữ liệu
    /**
     * lấy dữ liệu doanh thu 6 thang gan nhat
     * createdBy:: namnguyen(20/12/2022)
     */
    getLast6MonthsRenvenue() {
      try {
        let me = this;
        // hiện loading
        BaseRequest.get(`Orders/getLast6MonthsRenvenue`)
          .then((response) => {
            // gán dữ liệu trả về từ api
            let str = response.data.split(',');
            str.forEach(item => {
              me.last6MonthsRenvenue.push(parseInt(item));
            });
            //me.last6MonthsRenvenue = .sp;
          })
          .catch((e) => {
            console.log(e);
          });
        //this.getLast7DaysRenvenue();
      } catch (error) {
        console.log(error);
      }
    },
    /**
     * lấy dữ liệu doanh thu 7 ngay gan nhat
     * createdBy:: namnguyen(20/12/2022)
     */
    getLast7DaysRenvenue() {
      try {
        let me = this;
        // hiện loading
        BaseRequest.get(`Orders/getLast7DaysRenvenue`)
          .then((response) => {
            // gán dữ liệu trả về từ api
            let str = response.data.split(',');
            str.forEach(item => {
              me.last7DaysRenvenue.push(parseInt(item));
            });
            //me.last6MonthsRenvenue = .sp;
          })
          .catch((e) => {
            console.log(e);
          });
        //this.getLast6MonthsRenvenue();
      } catch (error) {
        console.log(error);
      }
    },
    getLast7Days() {
      let arrDays = '0123456'.split('').map(function (n) {
        var d = new Date();
        d.setDate(d.getDate() - n);
        return (function (day, month, year) {
          return [day < 10 ? '0' + day : day, month < 10 ? '0' + month : month, year].join('/');
        })(d.getDate(), d.getMonth(), d.getFullYear());
      }).join(',');
      return arrDays.split(',').reverse();
    },
    getLast6Months() {
      let monthNames = ["Tháng 1", "Tháng 2", "Tháng 3", "Tháng 4", "Tháng 5", "Tháng 6", "Tháng 7", "Tháng 8", "Tháng 9", "Tháng 10", "Tháng 11", "Tháng 12"];
      let today = new Date();
      let d;
      let month;
      let result=[];
      for (let i = 6; i > 0; i -= 1) {
        d = new Date(today.getFullYear(), today.getMonth() - i + 1, 1);
        month = monthNames[d.getMonth()];
        result.push(month);
      }
      return result;
    }
  },
};
</script>

<style scoped>
#myChart {
  display: flex !important;
  width: 840px !important;
  height: 550px !important;
  margin-left: 180px;
}
#myChart2 {
  display: flex !important;
  width: 840px !important;
  height: 550px !important;
  margin-left: 180px;
}