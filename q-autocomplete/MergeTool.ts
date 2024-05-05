const branches: Fig.Generator = {
  script: ["git", "branch", "--no-color"],
  postProcess: (output) => {
    if (output.startsWith("fatal:")) {
      return [];
    }
    return output.split("\n").map((branch) => {
      return { name: branch.replace("*", "").trim(), description: "Branch" };
    });
  },
};

const completionSpec: Fig.Spec = {
  name: "MergeTool",
  description: "MergeTool is a tool for merging branches.",
  subcommands: [],
  options: [
    {
      name: ["--push", "-p"],
      description: "Push the changes after merging",
    },
    {
      name: ["--verbose", "-v"],
      description: "Show verbose output",
    },
    {
      name: ["--version", "-V"],
      description: "Show version information",
    },
    {
      name: ["--help", "-h", "-?"],
      description: "Show help and usage information",
    },
  ],
  args: {
    name: "TARGET_BRANCH",
    description: "The branch to merge into.",
    generators: branches
  },
};
export default completionSpec;