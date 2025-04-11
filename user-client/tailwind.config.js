/** @type {import('tailwindcss').Config} */
export default {
  content: ['./index.html', './src/**/*.{vue,js,ts,jsx,tsx}'],
  // purge: ['./index.html', './src/**/*.{vue,js,ts,jsx,tsx}'],
  theme: {
    extend: {
      // Mở rộng cấu hình cho grid-template-columns
      gridTemplateColumns: {
        // Tạo lớp grid-cols-16 (16 cột bằng nhau)
        '16': 'repeat(16, minmax(0, 1fr))',
        '18': 'repeat(18, minmax(0, 1fr))',
        '20': 'repeat(20, minmax(0, 1fr))',
        '22': 'repeat(22, minmax(0, 1fr))',
        // Tạo lớp grid-cols-24 (24 cột bằng nhau)
        '24': 'repeat(24, minmax(0, 1fr))',

        // Bố cục sidebar cố định + nội dung linh hoạt
        // Tạo lớp grid-cols-sidebar-fixed
        'sidebar-fixed': '250px minmax(0, 1fr)', // Sidebar 250px, phần còn lại tự co giãn

        // Bố cục sidebar linh hoạt + nội dung linh hoạt (ví dụ tỷ lệ 1:3)
        // Tạo lớp grid-cols-sidebar-fluid
        'sidebar-fluid': '1fr 3fr',

        // Bố cục cho danh sách card tự động xuống hàng
        // Tạo lớp grid-cols-auto-fit-150
        'auto-fit-150': 'repeat(auto-fit, minmax(150px, 1fr))', // Các cột tối thiểu 150px, tự lấp đầy

         // Tạo lớp grid-cols-auto-fill-200
        'auto-fill-200': 'repeat(auto-fill, minmax(200px, 1fr))', // Tương tự auto-fit nhưng giữ ô trống nếu còn chỗ

        // Bố cục tùy chỉnh với các đơn vị khác nhau
        // Tạo lớp grid-cols-custom-layout
        'custom-layout': '100px auto 1fr 2fr',
      },
      gridColumn: {
        'span-14': 'span 14 / span 14', // Ví dụ: Chiếm 14 cột
        'span-15': 'span 15 / span 15', // Chiếm 15 cột
        'span-16': 'span 16 / span 16', // Ví dụ: Chiếm 16 cột
        'span-20': 'span 20 / span 20', // Ví dụ: Chiếm 16 cột
        'span-18': 'span 18 / span 18', // Ví dụ: Chiếm 16 cột
        'span-24': 'span 24 / span 24', // Ví dụ: Chiếm 24 cột
        '2/10': '2 / 10',               // Bắt đầu từ đường kẻ 2, kết thúc trước đường kẻ 10
        '3/11': '3 / 11',               // Bắt đầu từ đường kẻ 3, kết thúc trước đường kẻ 11
        'full': '1 / -1',               // Chiếm từ đường kẻ đầu tiên (1) đến đường kẻ cuối cùng (-1)
      },
      // Mở rộng cho grid-column-start
      gridColumnStart: {
        '13': '13',
        '14': '14',
        '15': '15',
        '16': '16',
        '24': '24',
      },
      // Mở rộng cho grid-column-end
      gridColumnEnd: {
        '13': '13',
        '14': '14',
        '15': '15',
        '16': '16',
        '24': '24',
      }
    },
  },
  variants: {
    extend: {},
  },
  plugins: [],
}

