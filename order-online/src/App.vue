<template>
  <v-app>
    
    <router-view/>

    <!-- Phần view của lịch sử đơn hàng -->
    <history-order-right-sheet />
    <!-- End -->

    <!-- Phần view của tra cứu đơn hàng -->
    <order-right-sheet />
    <!-- End -->
  </v-app>
</template>

<script>
import HistoryOrderRightSheet from '@/components/right-sheets/HistoryOrderRightSheet.vue';
import OrderRightSheet from '@/components/right-sheets/OrderRightSheet.vue';

export default {
  components: { HistoryOrderRightSheet, OrderRightSheet },
  name: 'App',

  data: () => ({
    //
  }),
};
</script>
