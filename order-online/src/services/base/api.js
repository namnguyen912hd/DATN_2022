import http from "../../http";


class ApiService {
  /**
   * Lấy tất cả bản ghi
   * @param {*} apiRouter tên controller
   * @returns Tất cả bản ghi
   * Author: TTKien(6/12/2021)
   */
  get(apiRouter) {
    return http.get(`${apiRouter}`);
  }

  /**
   * Lấy bản ghi theo khoá chính
   * @param {*} id Khoá chính
   * @returns Bản ghi cần lấy
   * Author: TTKien(6/12/2021)
   */
  getId(apiRouter,id) {
    return http.get(`${apiRouter}/${id}`);
  }

  /**
   * Thêm mới 1 bản ghi
   * @param {*} apiRouter Tên controller
   * @param {*} obj bản ghi cần thêm
   * @returns Bản ghi được thêm mới
   */
  create(apiRouter, obj) {
    return http.post(`${apiRouter}`, obj);
  }

  /**
   * Sửa 1 bản ghi
   * @param {*} apiRouter Tên controller
   * @param {*} id Khoá chính tìm bản ghi cần sửa
   * @param {*} obj Thông tin cập nhật
   * @returns Sửa bản ghi thành công
   * Author: TTKien(6/12/2021)
   */
  update(apiRouter, id, obj) {
    return http.put(`${apiRouter}/${id}`, obj);
  }

   /**
   * Xoá 1 bản ghi
   * @param {*} apiRouter Tên controller
   * @param {*} id Khoá chính tìm bản ghi cần xoá
   * @returns Bản ghi được xoá thành công
   */
  delete(apiRouter, id) {
    return http.delete(`${apiRouter}/${id}`);
  }

  /**
   * Xoá nhiều bản ghi 1 bản ghi
   * @param {*} apiRouter Tên controller
   * @param {*} listId danh sách khoá chính của các bản ghi cần xoá
   * @returns Các bản ghi được xoá thành công
   */
  deleteMutilple(apiRouter, listId) {
    return http.delete(`${apiRouter}`, {data: listId});
  }


}

export default new ApiService();
