/** @type {import('tailwindcss').Config} */
module.exports = {
  content: ["./Views/**/*.cshtml", "./Views/*.cshtml","./Areas/**/Views/**"],
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