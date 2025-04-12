/** @type {import('tailwindcss').Config} */
export default {
  content: ['./index.html', './src/**/*.{vue,js,ts,jsx,tsx}'],
  // purge: ['./index.html', './src/**/*.{vue,js,ts,jsx,tsx}'],
  theme: {
    extend: {
      gridTemplateColumns: {
        '13': 'repeat(13, minmax(0, 1fr))',
        '14': 'repeat(14, minmax(0, 1fr))',
        '15': 'repeat(15, minmax(0, 1fr))',
        '16': 'repeat(16, minmax(0, 1fr))',
        '18': 'repeat(18, minmax(0, 1fr))',
        '20': 'repeat(20, minmax(0, 1fr))',
        '22': 'repeat(22, minmax(0, 1fr))',
        '24': 'repeat(24, minmax(0, 1fr))',
        'sidebar-fixed': '250px minmax(0, 1fr)',
        'sidebar-fluid': '1fr 3fr',
        'auto-fit-150': 'repeat(auto-fit, minmax(150px, 1fr))',
        'auto-fill-200': 'repeat(auto-fill, minmax(200px, 1fr))',
        'custom-layout': '100px auto 1fr 2fr',
      },
      gridColumn: {
        'span-13': 'span 13 / span 13', // Ví dụ: Chiếm 14 cột
        'span-14': 'span 14 / span 14', // Ví dụ: Chiếm 14 cột
        'span-15': 'span 15 / span 15', // Chiếm 15 cột
        'span-16': 'span 16 / span 16', // Ví dụ: Chiếm 16 cột
        'span-20': 'span 20 / span 20', // Ví dụ: Chiếm 16 cột
        'span-18': 'span 18 / span 18', // Ví dụ: Chiếm 16 cột
        'span-22': 'span 22 / span 22', // Ví dụ: Chiếm 16 cột
        'span-24': 'span 24 / span 24', // Ví dụ: Chiếm 24 cột
        '2/10': '2 / 10',               // Bắt đầu từ đường kẻ 2, kết thúc trước đường kẻ 10
        '3/11': '3 / 11',               // Bắt đầu từ đường kẻ 3, kết thúc trước đường kẻ 11
        'full': '1 / -1',               // Chiếm từ đường kẻ đầu tiên (1) đến đường kẻ cuối cùng (-1)
      },
      gridColumnStart: {
        '13': '13',
        '14': '14',
        '15': '15',
        '16': '16',
        '18': '18',
        '20': '20',
        '22': '22',
        '24': '24',
      },
      // Mở rộng cho grid-column-end
      gridColumnEnd: {
        '13': '13',
        '14': '14',
        '15': '15',
        '16': '16',
        '18': '18',
        '20': '20',
        '22': '22',
        '24': '24',
      }
    },
  },
  variants: {
    extend: {},
  },
  plugins: [],
}

