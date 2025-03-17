/** @type {import('tailwindcss').Config} */
module.exports = {
  content: ["./Views/**/*.cshtml", "./Views/*.cshtml"],
  theme: {
      extend: {
          zIndex: {
              '-1': '-1',
          },
          flexGrow: {
              '5': '5'
          }
      },
  }
}