const Formatter = () => {

  const dateToString = (date) => {
    if (!date) { return ""; }
    return date.split("T")[0];
  }

  return {
    dateToString
  }
}

export default Formatter;