const seatsContainer = document.getElementById('seat-container');
const cartItemsElement = document.getElementById('cart-items');
const seatCountElement = document.getElementById('seat-count');
const seatRowElement = document.getElementById('seat-row');
const seatPriceElement = document.getElementById('seat-price');
const movieNameElement = document.getElementById('movie-name');
const movieTimeElement = document.getElementById('movie-time');

const rows = ['A', 'B', 'C', 'D', 'E']; // Tên các hàng ghế
const seatsPerRow = 15;
const seatPrice = 10; // Giá vé cho mỗi ghế
const premiumSeatPrice = 15; // Giá vé cho ghế premium

let selectedSeats = [];
let cartItems = []; // Mảng chứa các mục trong giỏ hàng

// Hàm tạo ra hàng ghế và thêm vào container

function createSeats() {
  for (let i = 0; i < rows.length; i++) {
    const seatRow = document.createElement('div');
    seatRow.classList.add('seat-row', rows[i].toLowerCase());
    seatsContainer.appendChild(seatRow);

    for (let seatNum = 1; seatNum <= seatsPerRow; seatNum++) {
      const seat = document.createElement('div');
      seat.classList.add('seat');
      seat.innerText = rows[i] + seatNum;
      seatRow.appendChild(seat);

      // Lắng nghe sự kiện click cho mỗi ghế
      seat.addEventListener('click', selectSeat);
    }
  }
}

// Xử lý khi người dùng chọn ghế
function selectSeat() {
  if (this.classList.contains('selected')) {
    this.classList.remove('selected');
    const index = selectedSeats.indexOf(this.innerText);
    if (index > -1) {
      selectedSeats.splice(index, 1);
    }
  } else {
    if (selectedSeats.length < 8) {
      this.classList.add('selected');
      selectedSeats.push(this.innerText);
    } else {
      alert('Bạn không thể chọn nhiều hơn 8 ghế một lúc!');
    }
  }

  // Cập nhật thông tin giỏ hàng
  updateCart();
}

function updateCart() {
  cartItems = selectedSeats.map((seat, index) => {
    const rowIndex = rows.indexOf(seat.charAt(0));
    const isPremiumSeat = rowIndex >= rows.length - 3;
    const price = isPremiumSeat ? premiumSeatPrice : seatPrice;

    return {
        seatNumber: seat,
        seatRow: `${seat.charAt(0)}${index + 1}`, // Thêm số thứ tự của ghế vào seatRow
      price: price
    };
  });

  renderCart();
  updateCartTotal();
}

// Hàm hiển thị thông tin giỏ hàng
function renderCart() {
    cartItemsElement.innerHTML = ''; // Xóa nội dung hiện tại của giỏ hàng

    cartItems.forEach((item) => {
        const cartItem = document.createElement('div');
        cartItem.classList.add('cart-item');
        cartItemsElement.appendChild(cartItem);
    });
}

// Hàm cập nhật tổng tiền trong giỏ hàng
function updateCartTotal() {
    const total = cartItems.reduce((sum, item) => sum + item.price, 0);
    seatCountElement.textContent = selectedSeats.length;
    seatRowElement.textContent = cartItems.map((item) => item.seatNumber).join(', ');
    seatPriceElement.textContent = total;
}
// Hàm đặt vé
function checkout() {
    // TODO: Gửi dữ liệu đặt vé đến máy chủ
    alert('Bạn đã đặt vé thành công!');
}

// Gọi hàm tạo ghế khi trang được tải
createSeats();
//
