"use client";

import { useEffect, useState } from "react";

import { themes } from "../lib/theme";
import { BsPalette2 } from "react-icons/bs";
import useLocalStorage from "../lib/hooks/useLocalStorage";

const ThemeToggle = () => {
  const [hasMounted, setHasMounted] = useState(false);
  const [activeTheme, setActiveTheme] = useLocalStorage("theme", "light");

  // = useState("light");

  // Update the `data-theme` attribute on the `<body>` tag to globally update the theme
  useEffect(() => {
    document.querySelector("body").setAttribute("data-theme", activeTheme);
  }, [activeTheme]);

  useEffect(() => setHasMounted(true));

  return (
    <div className="dropdown dropdown-hover dropdown-end">
      <label tabIndex={0} className="btn m-1">
        <BsPalette2 className="w-6 h-6 text-primary" />
        {themes && activeTheme}
      </label>
      <ul
        tabIndex={0}
        className="dropdown-content z-[1] menu p-2 shadow bg-base-200 rounded-box w-52 h-56 flex-nowrap overflow-y-auto"
      >
        {themes &&
          themes.map((theme) => {
            return (
              <li
                key={theme.name}
                onClick={() => {
                  setActiveTheme(theme.value);
                }}
              >
                <span
                  data-theme={theme.value}
                  className="bg-transparent text-inherit hover:bg-base-100"
                >
                  {theme.name}
                </span>
              </li>
            );
          })}
      </ul>
    </div>
  );
};

export default ThemeToggle;
