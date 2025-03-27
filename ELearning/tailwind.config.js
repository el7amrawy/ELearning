import plugin from "tailwindcss/plugin";
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
    },
    plugins: [
        plugin(({ addUtilities }) => {
            addUtilities({
                ".field-sizing-content": {
                    "field-sizing": "content",
                },
            });
        }),
    ],
}