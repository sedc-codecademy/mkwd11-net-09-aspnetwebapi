import MenuItem from "@mui/material/MenuItem";
import FormControl from "@mui/material/FormControl";
import Select from "@mui/material/Select";
import { useState } from "react";

const HomePage = (props) => {
  const [year, setYear] = useState(2022);
  const [genre, setGenre] = useState("");
  const [movies, setMovies] = useState(props.user.movieList);

  const filterMovies = async () => {
    const result = await fetch(
      `http://localhost:5052/api/movies/filter?year=${year}&genre=${genre}`
    );
    const res = await result.json();
    setMovies(res);
  };
  return (
    <div>
      <div> Hi {props.user.fullName}</div>
      <div>
        <h2>Movies list:</h2>
        <div>
          <FormControl variant="standard" sx={{ m: 1, minWidth: 120 }}>
            <Select
              labelId="demo-simple-select-standard-label"
              id="demo-simple-select-standard"
              value={genre}
              onChange={(e) => setGenre(e.target.value)}
              label="Genre"
            >
              <MenuItem value={1}>Comedy</MenuItem>
              <MenuItem value={2}>Action</MenuItem>
              <MenuItem value={3}>Romance</MenuItem>
            </Select>
            <input
              type="number"
              value={year}
              onChange={(e) => setYear(e.target.value)}
            />
            <button onClick={filterMovies}>Search</button>
          </FormControl>
        </div>
        <div>
          {movies.map((movie) => (
            <div
              style={{
                border: "1px solid black",
                padding: "10px",
                margin: "10px",
              }}
            >
              <p>Name: {movie.title}</p>
              <p>
                Description:
                {movie.description
                  ? movie.description
                  : "No description provided!"}
              </p>
              <p>Year: {movie.year}</p>
            </div>
          ))}
        </div>
      </div>
    </div>
  );
};

export default HomePage;
