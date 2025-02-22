namespace TodoListWpf.Tests
{
    [TestFixture]
    public class TaskServiceTests
    {
        private ITaskService _taskService;
        private string _path;

        [SetUp]
        public void Setup()
        {
            _path = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString() + ".json");
            _taskService = new TaskService(_path);
        }

        [TearDown]
        public void TearDown()
        {
            if (File.Exists(_path))
            {
                File.Delete(_path);
            }
        }

        [Test]
        public void AddTask_WithValidData_ReturnsSuccess()
        {
            const string title = "Test Task";

            OperationResult result = _taskService.AddTask(title, TaskType.Work, false);

            Assert.Multiple(() =>
            {
                Assert.That(result.IsSuccess, Is.True, result.ErrorMessage);
                Assert.That(_taskService.Tasks.Count, Is.EqualTo(1));
                Assert.That(_taskService.Tasks.Any(task => task.Title.Equals(title, StringComparison.OrdinalIgnoreCase)), Is.True);
            });
        }

        [Test]
        public void AddTask_WithEmptyTitle_ReturnsFailure()
        {
            const string title = "";

            OperationResult result = _taskService.AddTask(title, TaskType.Work, false);

            Assert.That(result.IsSuccess, Is.False);
        }

        [Test]
        public void AddTask_WithDuplicateTitle_ReturnsFailure()
        {
            TaskItem task = new(Id: Guid.NewGuid(), Title: "Duplicate Task", Type: TaskType.Work, IsDone: false);

            OperationResult resultSuccess = _taskService.AddTask(task.Title, task.Type, task.IsDone);
            OperationResult resultFailure = _taskService.AddTask(task.Title, task.Type, task.IsDone);

            Assert.Multiple(() =>
            {
                Assert.That(resultSuccess.IsSuccess, Is.True);
                Assert.That(resultFailure.IsSuccess, Is.False);
            });
        }

        [Test]
        public void UpdateTask_WithValidData_ReturnsSuccess()
        {
            _taskService.AddTask(title: "Old Task", type: TaskType.Work, isDone: false);
            TaskItem taskOld = _taskService.Tasks.First();

            OperationResult result = _taskService.UpdateTask(taskOld, title: "Updated Task", type: TaskType.Personal, isDone: true);

            TaskItem taskUpdated = _taskService.Tasks.First();
            Assert.Multiple(() =>
            {
                Assert.That(result.IsSuccess, Is.True, result.ErrorMessage);
                Assert.That(taskUpdated.Title, Is.EqualTo("Updated Task"));
                Assert.That(taskUpdated.Type, Is.EqualTo(TaskType.Personal));
                Assert.That(taskUpdated.IsDone, Is.True);
            });
        }

        [Test]
        public void UpdateTask_TaskNotFound_ReturnsFailure()
        {
            TaskItem fakeTask = new(Id: Guid.NewGuid(), Title: "Non-existing task", Type: TaskType.Work, IsDone: false);

            OperationResult result = _taskService.UpdateTask(fakeTask, "New Title", TaskType.Personal, true);

            Assert.That(result.IsSuccess, Is.False);
        }

        [Test]
        public void DeleteTask_WithValidTask_ReturnsSuccess()
        {
            const string title = "Task to Delete";
            _taskService.AddTask(title, type: TaskType.University, isDone: false);
            int countBeforeDelete = _taskService.Tasks.Count;

            TaskItem task = _taskService.Tasks.First(x => x.Title.Equals(title, StringComparison.OrdinalIgnoreCase));
            OperationResult result = _taskService.DeleteTask(task);

            Assert.Multiple(() =>
            {
                Assert.That(result.IsSuccess, Is.True, result.ErrorMessage);
                Assert.That(_taskService.Tasks.Count, Is.EqualTo(countBeforeDelete - 1));
                Assert.That(_taskService.Tasks.Any(x => x.Title.Equals(title, StringComparison.OrdinalIgnoreCase)), Is.False);
            });
        }

        [Test]
        public void DeleteTask_TaskNotFound_ReturnsFailure()
        {
            TaskItem fakeTask = new(Id: Guid.NewGuid(), Title: "Non-existing task", Type: TaskType.Work, IsDone: false);

            OperationResult result = _taskService.DeleteTask(fakeTask);

            Assert.That(result.IsSuccess, Is.False);
        }

        [Test]
        public void SaveAndLoadTasks_PersistsDataCorrectly()
        {
            const string title = "Persinent task";
            _taskService.AddTask(title, type: TaskType.Work, isDone: true);
            int countBeforeSave = _taskService.Tasks.Count;

            OperationResult saveResult = _taskService.SaveTasks();
            Assert.That(saveResult.IsSuccess, Is.True, saveResult.ErrorMessage);

            _taskService.Tasks.Clear();
            Assert.That(_taskService.Tasks, Is.Empty);

            OperationResult loadResult = _taskService.LoadTasks();
            Assert.Multiple(() =>
            {
                Assert.That(loadResult.IsSuccess, Is.True, loadResult.ErrorMessage);
                Assert.That(_taskService.Tasks.Count, Is.EqualTo(countBeforeSave));
                Assert.That(_taskService.Tasks.Any(x => x.Title.Equals(title, StringComparison.OrdinalIgnoreCase) && x.Type == TaskType.Work && x.IsDone), Is.True);
            });
        }
    }
}
