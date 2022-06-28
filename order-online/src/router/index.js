import Vue from "vue";
import VueRouter from "vue-router";
import Home from "../views/pages/Home.vue";
import Complete from "../views/pages/Complete.vue";
import Payment from "../views/pages/Payment.vue";

Vue.use(VueRouter);

const routes = [
  {
    path: "/",
    name: "Home",
    component: Home,
  },
  { path: "/home", redirect: { name: "Home" } },
  {
    path: "/Complete",
    name: "Complete",
    component: Complete,
  },
  {
    path: "/Payment",
    name: "Payment",
    component: Payment,
  },
];

const router = new VueRouter({
  mode: "history",
  base: process.env.BASE_URL,
  routes,
});

export default router;
